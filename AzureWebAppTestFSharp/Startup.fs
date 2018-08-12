namespace AzureWebAppTestFSharp

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Swashbuckle.AspNetCore.Swagger
open System.Threading.Tasks


type Startup private () =
    new (configuration: IConfiguration) as this =
        Startup() then
        this.Configuration <- configuration

    member this.ConfigureServices(services: IServiceCollection) =
        services.AddMvc() |> ignore
        services.AddSwaggerGen(fun c -> c.SwaggerDoc("v1", Info( Title = "My API", Version = "v1")))
        |> ignore

    member this.Configure(app: IApplicationBuilder, env: IHostingEnvironment) =
        app.UseMvc() |> ignore
        app.UseSwagger() |> ignore
        app.UseSwaggerUI(fun c -> c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1")) |> ignore
        app.Run(fun c -> c.Response.Redirect("swagger"); Task.CompletedTask)
        |> ignore

    member val Configuration : IConfiguration = null with get, set