using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EFCore.Performance.xUnit
{
	public sealed class UnitTest1
	{
		[Fact]
		public Task Test1() => Execute();

		[Fact]
		public Task Test2() => Execute();

		[Fact]
		public Task Test3() => Execute();

		[Fact]
		public Task Test4() => Execute();

		[Fact]
		public Task Test5() => Execute();

		static async Task Execute()
		{
			using var host = new HostBuilder()
			                 .ConfigureServices(x =>
			                                    {
				                                    x.AddSingleton<IServer, TestServer>()
				                                     .AddDbContext<Storage>(builder => builder
				                                                                       .UseInMemoryDatabase(Guid
				                                                                                            .NewGuid()
				                                                                                            .ToString())
				                                                                       .EnableSensitiveDataLogging())
				                                     .AddIdentityCore<User>()
				                                     .AddEntityFrameworkStores<Storage>();
			                                    })
			                 .Build();
			await host.StartAsync();

			await host.Services.GetRequiredService<UserManager<User>>()
			          .CreateAsync(new User {UserName = "SomeUser"}, "*Password10*");
		}
	}
}