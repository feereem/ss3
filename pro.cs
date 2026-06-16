// อะไรไม่รู้
builder.Services.AddDbContext<BelleCroissantLyonnaisContext>(c =>
{
    c.UseSqlServer(builder.Configuration.GetConnectionString("DC"));
});
// กันลูป
builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
// ล็อก
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Basic"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/swagger"))
    {
        await next(); return;
    }
    var a = context.Request.Headers["Authorization"].ToString();
    if (a.StartsWith("basic"))
    {
        var c = Encoding.UTF8.GetString(Convert.FromBase64String(a[6..])).Split(':');
        if (c[0] == "staff" &&  c[1] == "BCLyon2024")
        {
            await next();
            return;
        }
    }
    context.Response.StatusCode = 401;
    await context.Response.WriteAsync("Unauthorized : Access Denied");
});
