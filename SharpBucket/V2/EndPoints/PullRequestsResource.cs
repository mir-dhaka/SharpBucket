﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SharpBucket.Utility;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// Manage pull requests for a repository. Use this resource to perform CRUD (create/read/update/delete) operations on a pull request. 
    /// More info:
    /// https://confluence.atlassian.com/display/BITBUCKET/pullrequests+Resource
    /// </summary>
    public class PullRequestsResource
    {
        private readonly RepositoriesEndPoint _repositoriesEndPoint;
        private readonly string _slug;
        private readonly string _accountName;

        #region Pull Requests Resource

        public PullRequestsResource(string accountName, string repoSlugOrName, RepositoriesEndPoint repositoriesEndPoint)
        {
            _accountName = accountName.GuidOrValue();
            _slug = repoSlugOrName.ToSlug();
            _repositoriesEndPoint = repositoriesEndPoint;
        }

        /// <summary>
        /// List open pull requests on the repository.
        /// </summary>
        /// <returns></returns>
        public List<PullRequest> ListPullRequests()
            => ListPullRequests(new ListPullRequestsParameters());

        /// <summary>
        /// List open pull requests on the repository.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        /// <returns></returns>
        [Obsolete("Prefer the ListPullRequests(ListPullRequestsParameters) overload.")]
        public List<PullRequest> ListPullRequests(ListParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            return _repositoriesEndPoint.ListPullRequests(_accountName, _slug, parameters);
        }

        /// <summary>
        /// List pull requests on the repository.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        public List<PullRequest> ListPullRequests(ListPullRequestsParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            return _repositoriesEndPoint.ListPullRequests(_accountName, _slug, parameters);
        }

        /// <summary>
        /// Enumerate open pull requests on the repository.
        /// </summary>
        public IEnumerable<PullRequest> EnumeratePullRequests()
            => EnumeratePullRequests(new EnumeratePullRequestsParameters());

        /// <summary>
        /// Enumerate pull requests on the repository.
        /// </summary>
        /// <param name="parameters">Parameters for the queries.</param>
        public IEnumerable<PullRequest> EnumeratePullRequests(EnumeratePullRequestsParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            return _repositoriesEndPoint.EnumeratePullRequests(_accountName, _slug, parameters);
        }

#if CS_8
        /// <summary>
        /// Enumerate open pull requests on the repository asynchronously, doing requests page by page.
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<PullRequest> EnumeratePullRequestsAsync(CancellationToken token = default)
            => EnumeratePullRequestsAsync(new EnumeratePullRequestsParameters(), token);

        /// <summary>
        /// Enumerate pull requests on the repository asynchronously, doing requests page by page.
        /// </summary>
        /// <param name="parameters">Parameters for the queries.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<PullRequest> EnumeratePullRequestsAsync(EnumeratePullRequestsParameters parameters, CancellationToken token = default)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            return _repositoriesEndPoint.EnumeratePullRequestsAsync(_accountName, _slug, parameters, token);
        }
#endif

        /// <summary>
        /// Creates a new pull request. The request URL you provide is the destination repository URL. 
        /// For this reason, you must specify an explicit source repository in the request object if you want to pull from a different repository.
        /// </summary>
        /// <param name="pullRequest">The pull request.</param>
        /// <returns></returns>
        public PullRequest PostPullRequest(PullRequest pullRequest)
        {
            return _repositoriesEndPoint.PostPullRequest(_accountName, _slug, pullRequest);
        }

        /// <summary>
        /// Creates a new pull request. The request URL you provide is the destination repository URL. 
        /// For this reason, you must specify an explicit source repository in the request object if you want to pull from a different repository.
        /// </summary>
        /// <param name="pullRequest">The pull request.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<PullRequest> PostPullRequestAsync(PullRequest pullRequest, CancellationToken token = default)
        {
            return await _repositoriesEndPoint.PostPullRequestAsync(_accountName, _slug, pullRequest, token);
        }

        /// <summary>
        /// Updates an existing pull request. The pull request's status must be open. 
        /// With the exception of the source and destination parameters, the request body must include all the existing request parameters; 
        /// Omitting a parameter causes the server to drop the existing value. For example, if the pull requests already has 3 reviewers, 
        /// the request body must include these 3 reviewers to prevent Bitbucket from dropping them.
        /// </summary>
        /// <param name="pullRequest">The pull request.</param>
        /// <returns></returns>
        public PullRequest PutPullRequest(PullRequest pullRequest)
        {
            return _repositoriesEndPoint.PutPullRequest(_accountName, _slug, pullRequest);
        }

        /// <summary>
        /// Updates an existing pull request. The pull request's status must be open. 
        /// With the exception of the source and destination parameters, the request body must include all the existing request parameters; 
        /// Omitting a parameter causes the server to drop the existing value. For example, if the pull requests already has 3 reviewers, 
        /// the request body must include these 3 reviewers to prevent Bitbucket from dropping them.
        /// </summary>
        /// <param name="pullRequest">The pull request.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<PullRequest> PutPullRequestAsync(PullRequest pullRequest, CancellationToken token = default)
        {
            return await _repositoriesEndPoint.PutPullRequestAsync(_accountName, _slug, pullRequest, token);
        }

        /// <summary>
        /// Returns activity log for all the pull requests on the repository.
        /// </summary>
        [Obsolete("Use GetPullRequestsActivities instead (naming improved, and max parameter exposed)")]
        public List<Activity> GetPullRequestLog()
            => GetPullRequestsActivities();

        /// <summary>
        /// Returns activity log for all the pull requests on the repository.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        public List<Activity> GetPullRequestsActivities(int max = 0)
        {
            return _repositoriesEndPoint.GetPullRequestsActivities(_accountName, _slug, max);
        }

        /// <summary>
        /// Enumerate activity log for all the pull requests on the repository.
        /// </summary>
        /// <param name="pageLen">The size of a page. If not defined the default page length will be used.</param>
        public IEnumerable<Activity> EnumeratePullRequestsActivities(int? pageLen = null)
        {
            return _repositoriesEndPoint.EnumeratePullRequestsActivities(_accountName, _slug, pageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate open pull requests on the repository asynchronously, doing requests page by page.
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<Activity> EnumeratePullRequestsActivitiesAsync(CancellationToken token = default)
            => EnumeratePullRequestsActivitiesAsync(null, token);

        /// <summary>
        /// Enumerate pull requests on the repository asynchronously, doing requests page by page.
        /// </summary>
        /// <param name="pageLen">The size of a page. If not defined the default page length will be used.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<Activity> EnumeratePullRequestsActivitiesAsync(int? pageLen, CancellationToken token = default)
        {
            return _repositoriesEndPoint.EnumeratePullRequestsActivitiesAsync(_accountName, _slug, pageLen, token);
        }
#endif

        #endregion

        #region Pull request Resource

        /// <summary>
        /// Get the Pull Request Resource.
        /// BitBucket does not have this Resource so this is a "Virtual" resource
        /// which offers easier manipulation of a specific Pull Request.
        /// </summary>
        /// <param name="pullRequestId">The pull request identifier.</param>
        /// <returns></returns>
        public PullRequestResource PullRequestResource(int pullRequestId)
        {
            return new PullRequestResource(_accountName, _slug, pullRequestId, _repositoriesEndPoint);
        }

        #endregion
    }
}
