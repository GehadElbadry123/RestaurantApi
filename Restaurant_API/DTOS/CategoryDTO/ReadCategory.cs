namespace Restaurant_API.DTOS.CategoryDTO
{
    public class ReadCategory
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int? numofProducts { get; set; }
    }
}
