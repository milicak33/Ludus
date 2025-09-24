namespace XOGameService.API.Dtos
{
    public class CreateGameDto
    {
        public required string PlayerXId { get; set; }
        public required string PlayerYId { get; set; }
    }
}
