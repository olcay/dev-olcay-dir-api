namespace WebApi.Models
{
    public class EnumDto
    {
        public string Value { get; set; }

        public string Text { get; set; }
    }

    public class EnumDto<T>
    {
        public T Value { get; set; }

        public string Text { get; set; }
    }
}
