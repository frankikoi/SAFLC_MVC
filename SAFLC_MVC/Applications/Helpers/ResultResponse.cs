namespace SAFLC_MVC.Applications.Helpers
{
    public class ResultResponse<T>
    {
        public bool Success { get; set; }

        public string? Message { get; set; }

        public T? Item { get; set; }

        public List<string> Errors { get; set; } = new();
    }
}
