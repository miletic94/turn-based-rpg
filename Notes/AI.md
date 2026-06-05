A good “simple but feels smart” AI usually comes from one idea:

> Don’t simulate intelligence. Just score moves with a few human-like heuristics.

You don’t need minimax or deep prediction. You just need a **utility function** that roughly answers:
“how good does this move feel right now?”

---

# 1. Core idea: score each move

Instead of “choosing”, AI does:

```csharp
bestMove = maxScore(allMoves)
```

So you define:

```csharp id="ai1"
float ScoreMove(Move move, Combatant self, Combatant enemy)
```

---

# 2. What matters in your system

You said moves can:

* Deal damage
* Heal
* Modify stats (attack / magic / defense)
* Have cost

So scoring should consider:

### Offense

* expected damage
* enemy defense reduction
* current enemy HP (finisher behavior)

### Defense / survival

* healing value
* defensive buffs
* self HP state (low HP → heal more valuable)

### Buffs / debuffs

* attack/magic buffs → future damage scaling
* defense buffs → survival scaling

### Cost

* discourage expensive moves unless worth it

---

# 3. Simple scoring model

## Base structure

```csharp id="ai2"
float ScoreMove(Move move, Combatant self, Combatant enemy)
{
    float score = 0f;

    score += EvaluateDamage(move, self, enemy);
    score += EvaluateHealing(move, self);
    score += EvaluateBuffs(move, self, enemy);
    score -= EvaluateCost(move, self);

    return score;
}
```

---

# 4. Damage evaluation

Very simple expected damage model:

```csharp id="ai3"
float EvaluateDamage(Move move, Combatant self, Combatant enemy)
{
    float dmg = move.GetExpectedDamage(self, enemy);

    // finishing logic
    float enemyHpRatio = enemy.Health / enemy.MaxHealth;
    if (enemyHpRatio < 0.25f)
        dmg *= 1.5f;

    return dmg * 1.0f;
}
```

---

# 5. Healing evaluation

```csharp id="ai4"
float EvaluateHealing(Move move, Combatant self)
{
    float heal = move.GetHealingValue(self);

    float hpRatio = self.Health / self.MaxHealth;

    // more valuable when low HP
    float urgency = 1f - hpRatio;

    return heal * (1f + urgency * 2f);
}
```

---

# 6. Buff / debuff evaluation

This is where “smart feeling” comes from.

```csharp id="ai5"
float EvaluateBuffs(Move move, Combatant self, Combatant enemy)
{
    float value = 0f;

    foreach (var effect in move.Effects)
    {
        if (effect.Type == EffectType.StatModifier)
        {
            float magnitude = effect.Value;

            switch (effect.Stat)
            {
                case Stat.Attack:
                    value += magnitude * 10f;
                    break;

                case Stat.Magic:
                    value += magnitude * 10f;
                    break;

                case Stat.Defense:
                    value += magnitude * 8f;
                    break;
            }

            // debuffs are slightly more valuable
            if (effect.Target == TargetType.Enemy)
                value *= 1.2f;
        }
    }

    return value;
}
```

---

# 7. Cost evaluation (VERY important)

This prevents spam of strong moves.

```csharp id="ai6"
float EvaluateCost(Move move, Combatant self)
{
    float cost = move.Cost;

    float maxResource = self.MaxEnergy;
    float ratio = cost / maxResource;

    // exponential penalty makes AI prefer efficiency
    return ratio * ratio * 20f;
}
```

---

# 8. Final decision logic

```csharp id="ai7"
public Move ChooseMove(Combatant self, Combatant enemy)
{
    Move bestMove = null;
    float bestScore = float.NegativeInfinity;

    foreach (var move in self.Moves)
    {
        float score = ScoreMove(move, self, enemy);

        if (score > bestScore)
        {
            bestScore = score;
            bestMove = move;
        }
    }

    return bestMove;
}
```

---

# 9. What this gives you in practice

Even this simple system produces behavior like:

### Early game

* buffs + setup moves

### Low HP enemy

* finishing moves

### Low HP self

* healing / defense

### No resources

* cheap moves

### Strong enemy buffing

* occasional debuffs

And it *feels intentional* even though it's just math.

---

# 10. Optional upgrade (makes it feel MUCH smarter)

Add randomness weighted by score:

```csharp id="ai8"
float temperature = 5f;

float probability = Mathf.Exp(score / temperature);
```

or just:

```csharp
if (Random.value < 0.1f) pick second best move
```

This avoids deterministic AI feeling robotic.

---

# Key takeaway

You don’t build “intelligent AI” here.

You build:

> a scoring system that encodes design intent

That’s why this works well in games:

* easy to tune
* predictable
* designer-friendly
* no ML required

---

If you want, I can extend this into:

* “aggressive / defensive / random personalities”
* or a system that learns player behavior over time
* or how to plug this cleanly into your current IMoveProvider setup
