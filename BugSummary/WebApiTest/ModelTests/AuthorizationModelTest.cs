using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
[ExcludeFromCodeCoverage]
public class AuthorizationModelTest
{
    [TestMethod]
    public void CreateModel()
    {
        AuthorizationModel model = new AuthorizationModel
        {
           token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpX",
           rol = 3
        };
        Assert.IsNotNull(model);
    }
    
}