using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using VimeoDotNet.Constants;
using VimeoDotNet.Exceptions;
using VimeoDotNet.Models;

namespace VimeoDotNet
{
    public partial class VimeoClient
    {
        /// <inheritdoc />
        public async Task AssignPresetToVideoAsync(long clipId, long presetId)
        {
            try
            {
                ThrowIfUnauthorized();

                var request = _apiRequestFactory.GetApiRequest(AccessToken);
                request.Method = HttpMethod.Put;
                request.Path = Endpoints.VideoPreset;
                request.UrlSegments.Add("clipId", clipId.ToString());
                request.UrlSegments.Add("presetId", presetId.ToString());

                var response = await request.ExecuteRequestAsync();
                UpdateRateLimit(response);
                CheckStatusCodeError(response, "Cannot assign preset");

                //return response.Content;
            }
            catch (Exception ex)
            {
                if (ex is VimeoApiException)
                {
                    throw;
                }

                throw new VimeoApiException("Cannot create tag.", ex);
            }
        }
    }
}