using Craftify.Application;
using Craftify.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{



    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}


var app = builder.Build();
{

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}

