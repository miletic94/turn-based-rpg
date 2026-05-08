using UnityEngine;

public class XpController
{
    private readonly XpService _xpService;
    private readonly XpView _xpView;
    public XpController(XpService xpService, XpView xpView)
    {
        _xpService = xpService;
        _xpView = xpView;
    }

    public async Awaitable<UpdateXpResult> UpdateXp()
    {
        var result = _xpService.UpdateXp();

        await _xpView.UpdateXp(result);

        return result;
    }
    public async Awaitable<UpdateXpResult> Initialize()
    {
        return await UpdateXp();
    }
}