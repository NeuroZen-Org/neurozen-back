namespace neurozen.API.Professionals.Domain.Model.Aggregates;

public partial class Professional
{
    public int Id { get; private set; }
    public string Name { get; set; }
    public string Specialty { get; set; }
    public string Experience { get; set; }
    public int Rating { get; set; }
    public int Reviews { get; set; }
    public int Price { get; set; }
    public string Availability { get; set; }
    public string Bio { get; set; }
    public string Image { get; set; }
}