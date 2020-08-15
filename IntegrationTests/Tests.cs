using IntegrationTests.Models;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{ 
    public class Tests
    {
        private RestClient _restClient;
        private Author _actualAuthor;

        [SetUp]
        public void Setup()
        {
            _restClient = new RestClient();
            _restClient.BaseUrl = new Uri("https://libraryjuly.azurewebsites.net");
            
        }

        
        [Test, Order(1)]
        public void PostAuthor()
        {
            var author = new Author();
            author.CreateAuthorRequest("Deive", "Jim2", "Drama");
            var json = author.ToJson();

            var request = new RestRequest("/api/authors", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(json);

            var response = _restClient.Execute(request);
            var responseToString = response.Content.ToString();
            var actualAuthor = Author.FromJson(responseToString);
            _actualAuthor = Author.FromJson(response.Content);
            var expectedAuthor = new Author();
            expectedAuthor.CreateAuthorRespons(author.FirstName, author.LastName, author.Genre);

            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual(expectedAuthor.Name, actualAuthor.Name);
        }

        [Test, Order(2)]
        public void GetAuthor()
        {

            var request = new RestRequest($"/api/authors/{ _actualAuthor.Id}");
            var response = _restClient.Get(request);

            var author = Author.FromJson(response.Content);

            Assert.IsTrue(response.IsSuccessful);
        }

        [Test, Order(3)]
        public void DeleteAuthor()
        {

            var request = new RestRequest($"/api/authors/{ _actualAuthor.Id}");
            var response = _restClient.Delete(request);

            Assert.IsTrue(response.IsSuccessful);
        }


    }
}