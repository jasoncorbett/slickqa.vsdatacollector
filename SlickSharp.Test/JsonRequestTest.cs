// Copyright 2012 AccessData Group, LLC.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//  http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlickQA.SlickSharp.Web;

namespace SlickQA.SlickSharp.Test
{
    [TestClass]
    public sealed class JsonRequestTest
    {
        [TestMethod]
        public void Uses_correct_content_type()
        {
            // ReSharper disable ObjectCreationAsStatement
            var req = new DummyWebRequest();


            new JsonRequest(req);

            Assert.AreEqual("application/json", req.ContentType);
            // ReSharper restore ObjectCreationAsStatement
        }

        #region Nested type: DummyWebRequest

        private sealed class DummyWebRequest : WebRequest
        {
            public override string ContentType { get; set; }
        }

        #endregion

        //[TestMethod]
        //[ExpectedException(typeof(NotFoundException))]
        //public void Throws_not_found_exception_for_404_error_code()
        //{
        //    var mockRequest = new Mock<WebRequest>();
        //    var mockResponse = new Mock<IHttpWebResponse>();

        //    mockResponse.SetupGet(r => r.StatusCode).Returns(HttpStatusCode.NotFound);
        //    mockRequest.Setup(request => request.GetResponse()).Throws(new WebException("Not Found", null,
        //                                                                                WebExceptionStatus.ProtocolError,
        //                                                                                mockResponse.Object));

        //    var jsonReq = new JsonRequest(mockRequest.Object);

        //    jsonReq.GetResponse();
        //}
    }
}
