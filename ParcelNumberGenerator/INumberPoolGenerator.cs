namespace ParcelNumberGenerator
{
    public interface INumberPoolGenerator
    {
        int Generate();
        Mode Mode { get; set; }
    }
}