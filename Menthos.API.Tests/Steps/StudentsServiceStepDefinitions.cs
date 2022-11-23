using System.Net;
using System.Net.Mime;
using System.Text;
using Menthos.API.Menthos.Resources;
using Menthos.API.Security.Domain.Services.Communication;
using Menthos.API.Security.Resources;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using SpecFlow.Internal.Json;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace Menthos.API.Tests.Steps;

[Binding]
public sealed class StudentsServiceStepDefinitions : WebApplicationFactory<Program>
{

    private readonly WebApplicationFactory<Program> _factory;

    public StudentsServiceStepDefinitions(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    private HttpClient Client { get; set; }
    private Uri BaseUri { get; set; }
    
    private Task<HttpResponseMessage> Response { get; set; }

    
    [Given(@"the Endpoint https://localhost:(.*)/api/v(.*)/student/sign-up is available")]
    public void GivenTheEndpointHttpsLocalhostApiVStudentIsAvailable(int port, int version)
    {
        BaseUri = new Uri($"https://localhost:{port}/api/v{version}/student/sign-up");
        Client = _factory.CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = BaseUri });
    }
    
    
    [When(@"a Post Request is sent")]
    public void WhenAPostRequestIsSent(Table saveStudentResource)
    {
        var request = saveStudentResource.CreateSet<RegisterRequest>().First();
        var content = new StringContent(request.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
        Response = Client.PostAsync(BaseUri, content);
    }
    
    [Then(@"A Response is received with Status (.*)")]
    public void ThenAResponseIsReceivedWithStatus(int expectedStatus)
    {
        var expectedStatusCode = ((HttpStatusCode)expectedStatus).ToString();
        var actualStatusCode = Response.Result.StatusCode.ToString();
        
        Assert.Equal(expectedStatusCode, actualStatusCode);
    }
    
    [Then(@"a Student Resource is included in Response Body")]
    public async Task ThenAStudentResourceIsIncludedInResponseBody(Table expectedStudentResource)
    {
        var expectedResource = expectedStudentResource.CreateSet<StudentResource>().First();
        var responseData = await Response.Result.Content.ReadAsStringAsync();
        var resource = JsonConvert.DeserializeObject<StudentResource>(responseData);
        Assert.Equal(expectedResource.Name, resource.Name);
    }
    
    [Given(@"A Student is already stored")]
    public async void GivenATutorialIsAlreadyStored(Table saveTutorialResource)
    {
        var resource = saveTutorialResource.CreateSet<StudentResource>().First();
        var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
        Response = Client.PostAsync(BaseUri, content);
        var responseData = await Response.Result.Content.ReadAsStringAsync();
        var responseResource = JsonConvert.DeserializeObject<StudentResource>(responseData);
        Assert.Equal(resource.Name, responseResource.Name);
    }
}