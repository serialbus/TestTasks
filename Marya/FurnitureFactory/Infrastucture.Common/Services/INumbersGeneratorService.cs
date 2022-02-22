namespace Infrastructure.Common.Services
{
    public interface INumbersGeneratorService
    {
        ServiceStatus Status { get; }
        int GenerateOrderNumber();
    }
}
