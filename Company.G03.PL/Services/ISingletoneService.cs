namespace Company.G03.PL.Services
{
    public interface ISingletoneService
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
