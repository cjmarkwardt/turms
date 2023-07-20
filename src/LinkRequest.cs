namespace Markwardt.Turms;

public interface ILinkRequest
{
    ILink Accept(ILinkHandler handler);
    void Reject();
}