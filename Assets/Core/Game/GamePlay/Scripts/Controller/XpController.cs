public class XpController
{
    private readonly XpService _xpService;
    private readonly XpView _xpView;
    public XpController(XpService xpService)
    {
        _xpService = xpService;
    }

    public UpdateXpResult UpdateXp()
    {
        var result = _xpService.UpdateXp();

        _xpView.UpdateXp(result);

        return result;
    }
}