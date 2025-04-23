namespace BuyItPlatform.GatewayApi.Utility
{
    public class MicroservicesUrls
    {
        public string AuthApiUrl { get; private set; } = "";
        public string CommentsApiUrl { get; private set; } = "";
        public string ListingReportsApiUrl { get; private set; } = "";
        public string ListingsApiUrl { get; private set; } = "";
        public string UserRatingApiUrl { get; private set; } = "";
        public string UserReportingApiUrl { get; private set; } = "";

        private readonly IConfiguration configuration;
        public MicroservicesUrls(IConfiguration configuration)
        {
            this.configuration = configuration;

            AuthApiUrl = configuration.GetValue<string>("ServiceUrls:AuthApi")!;
            CommentsApiUrl = configuration.GetValue<string>("ServiceUrls:CommentsApi")!;
            ListingReportsApiUrl = configuration.GetValue<string>("ServiceUrls:ListingReportsApi")!;
            ListingsApiUrl = configuration.GetValue<string>("ServiceUrls:ListingsApi")!;
            UserRatingApiUrl = configuration.GetValue<string>("ServiceUrls:UserRatingApi")!;
            UserReportingApiUrl = configuration.GetValue<string>("ServiceUrls:UserReportingApi")!;
        }
    }
}
