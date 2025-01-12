using System;
using System.Text.Json.Nodes;
using OrderBouncer.Application.Interfaces.Extractors;

namespace OrderBouncer.Application.Services.Extractors;

public class JsonExtractor : IJsonExtractor
{
    private readonly IEnumerable<IJsonExtractorProfile> _jsonExtractorProfiles;

    public JsonExtractor (IEnumerable<IJsonExtractorProfile> jsonExtractorProfiles){
        _jsonExtractorProfiles = jsonExtractorProfiles;
    }
    public async Task<JsonNode?> Extract<TProfile>(JsonNode json) where TProfile: class
    {
        IJsonExtractorProfile? jsonExtractorProfile = _jsonExtractorProfiles.FirstOrDefault(profile => profile.GetType() == typeof(TProfile));
        if(jsonExtractorProfile is null){
            return null;
        }

        return await jsonExtractorProfile.GetProfilePart(json);
    }
}
