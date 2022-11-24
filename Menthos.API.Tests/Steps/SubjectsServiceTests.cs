using System.Net;
using System.Net.Mime;
using System.Text;
using Menthos.API.Menthos.Resources;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using SpecFlow.Internal.Json;
using TechTalk.SpecFlow.Assist;
using Xunit; 

namespace Menthos.API.Tests.Steps;

[Binding]
public sealed class SubjectsServiceStepDefinitions : WebApplicationFactory<Program>
{
    private readonly WebApplicationFactory<Program> _factory;

    public SubjectsServiceStepDefinitions(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    private HttpClient Client { get; set; }
    private Uri BaseUri { get; set; }
    
    private Task<HttpResponseMessage> Response { get; set; }
    

    [Given(@"the Endpoint https://localhost:(.*)/api/v(.*)/subjects is available")]
    public void GivenTheEndpointHttpsLocalhostApiVSubjectsIsAvailable(int port, int version)
    {
        BaseUri = new Uri($"https://localhost:{port}/api/v{version}/subjects");
        Client = _factory.CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = BaseUri });
    }


    [When(@"a Post Request is sent")]
    public void WhenAPostRequestIsSent(Table saveSubjectResource)
    {
        var resource = saveSubjectResource.CreateSet<SaveSubjectResource>().First();
        var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
        Response = Client.PostAsync(BaseUri, content);
    }

    [Then(@"A Response is received with Status (.*)")]
    public void ThenAResponseIsReceivedWithStatus(int expectedStatus)
    {
        var expectedStatusCode = ((HttpStatusCode)expectedStatus).ToString();
        var actualStatusCode = Response.Result.StatusCode.ToString();
        
        Assert.Equal(expectedStatusCode, actualStatusCode);
    }

    [Then(@"a Subject Resource is included in Response Body")]
    public async Task ThenASubjectResourceIsIncludedInResponseBody(Table expectedSubjectResource)
    {
        var expectedResource = expectedSubjectResource.CreateSet<SubjectResource>().First();
        var responseData = await Response.Result.Content.ReadAsStringAsync();
        var resource = JsonConvert.DeserializeObject<SubjectResource>(responseData);
        Assert.Equal(expectedResource.Name, resource.Name);
    }
}