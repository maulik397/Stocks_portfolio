// bring finnhub service  

using StockAPIUsingHttpClient.Services;
// instalize obejct of webapplication that contain host ,looging ,service, configurations 
var builder = WebApplication.CreateBuilder(args);
/*
builder.Services	    Register services, middleware, dependencies
builder.Configuration	Read config from JSON, ENV, CLI, secrets
builder.Logging     	Set log levels, outputs
builder.Environment	    Detect environment and conditionally configure
builder.Host	        Configure the hosting infrastructure
 */
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
    logging.SetMinimumLevel(LogLevel.Information);
});

// enables mvc architecture  and let u use  controller classes and .cshtml views 
builder.Services.AddControllersWithViews();
/* enables httpclientfactory means  ,httpclient is used to send http client request and receive response from apis
 resgestering http client service in ioc container so that i can use it */
builder.Services.AddHttpClient();
/*
//adding custome service which i created  3 ways to register a service  
//( This tells the IoC  container how long to keep an instance alive after it’s been created.)
//transient create everytime when it is requested  scoped created once per http request 
// difference between scoped and transsient is clear that after one reuest if controlller or view reqquest service it get same data without page refresh 
// but  in transient if controller asked for service afeter that view asked for seervice both will be different  
*/
builder.Services.AddScoped<FinnhubService>();



//now setup for serivces,logging and config ( custome config is over ) now instalize object of webapplication 


// vThe middleware pipeline is initialized and ready to be configured.
var app = builder.Build();
/*
 Add middleware like UseRouting, UseAuthentication, etc.

Access environment info with app.Environment

Access configuration with app.Configuration

Access logging with app.Logger

Define routing/endpoints with MapGet, MapControllerRoute, etc.
m
 */
// Configure the HTTP request pipeline. 
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection(); //redirect to http req to https equivalent request 

app.UseStaticFiles();//add static files from wwwroot folder to browser 



app.UseRouting();//matches imcoming request to route endpoints defined in controller /home/index 

app.UseAuthorization(); // Checks whether a user is allowed to access a resource, based on the [Authorize] attribute firrst it check whther action method hass authorize or authnticate attribute 


//this is conventional route  attribute routing will bw in controller  
//It maps a default  route for your MVC controllers and call controller home with action methos index 


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.UseHttpLogging();  --> This middleware logs the HTTP requests and responses passing through your ASP.NET Core application.

app.Run();
