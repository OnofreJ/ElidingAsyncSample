using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run(typeof(GetUserInfoUseCase));

public interface IGetUserInfoUseCase
{
    Task<string> ExecuteAsync();

    Task<string> ExecuteElidingAsync();
}

[MemoryDiagnoser, RankColumn]
public class GetUserInfoUseCase : IGetUserInfoUseCase
{
    private readonly IUserData _data;

    public GetUserInfoUseCase() => _data = new UserData();

    [Benchmark]
    public async Task<string> ExecuteAsync()
    {
        return await _data.GetDataAsync();
    }

    [Benchmark]
    public Task<string> ExecuteElidingAsync()
    {
        return _data.GetDataAsync();
    }
}

public interface IUserData
{
    Task<string> GetDataAsync();
}

public class UserData : IUserData
{
    public async Task<string> GetDataAsync()
    {
        return await Task.FromResult($"User data returned at {DateTime.Now}");
    }
}