namespace DeviousCreation.CqrsStarterTemplate.Web.Features.Error
{
    public class ErrorViewModel
    {
        public ErrorViewModel()
        {
        }

        public ErrorViewModel(string requestId)
        {
            this.RequestId = requestId;
            if (!string.IsNullOrWhiteSpace(requestId))
            {
                this.ShowRequestId = true;
            }
        }

        public string RequestId { get; }

        public bool ShowRequestId { get; }
    }
}