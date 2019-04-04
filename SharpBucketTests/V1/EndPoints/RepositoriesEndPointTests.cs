﻿using System;
using System.Net;
using NUnit.Framework;
using SharpBucket;
using SharpBucketTests.V2;
using Shouldly;

namespace SharpBucketTests.V1.EndPoints
{
    [TestFixture]
    [Obsolete("test obsolete code")]
    public class RepositoriesEndPointTests
    {
        [Test]
        public void GetSrcFile_OfReadmeFile_GetFileContent()
        {
            var testRepoInfo = SampleRepositories.TestRepository.RepositoryInfo;
            var repositoryEndPoint = TestHelpers.GetV1ClientAuthenticatedWithOAuth()
                .RepositoriesEndPoint(testRepoInfo.AccountName, testRepoInfo.RepositoryName);

            var fileContent = repositoryEndPoint.GetSrcFile("master", "readme.md");

            fileContent.data.ShouldBe("This is a test repo generated by the SharpBucket unit tests");
        }

        [Test]
        public void GetSrcFile_OfAFileThatDoNotExists_ThrowExceptionWithStatusNotFound()
        {
            var testRepoInfo = SampleRepositories.TestRepository.RepositoryInfo;
            var repositoryEndPoint = TestHelpers.GetV1ClientAuthenticatedWithOAuth()
                .RepositoriesEndPoint(testRepoInfo.AccountName, testRepoInfo.RepositoryName);

            var exception = Assert.Throws<BitbucketException>(() => repositoryEndPoint.GetSrcFile("master", "FileThatDoNotExists.txt"));
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
            exception.Message.ShouldBe("Not Found");
        }
    }
}