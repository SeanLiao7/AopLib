namespace AOPLib.Model
{
    public interface IContextModel
    {
        object[ ] Args { get; set; }
        object Result { get; set; }
    }
}