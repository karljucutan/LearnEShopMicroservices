var builder = WebApplication.CreateBuilder(args);

// Add services tot he container.
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    //The Pre-Handler behaviors execute in the order they are added (FIFO).
    //The Post-Handler behaviors execute in the reverse order of how they were added (FILO).

    // Pre-Handler Method: The behaviors act in a queue fashion, executing in the order they were added (FIFO).
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
    // Post-Handler Method: They act in a stack fashion, executing in reverse order (FILO).
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();
