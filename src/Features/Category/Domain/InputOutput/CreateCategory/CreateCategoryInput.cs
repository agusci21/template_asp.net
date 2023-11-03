namespace Feature.Category;

public class CreateCategoryInput
{
    public string Name {get;}
    
    public CreateCategoryInput(string name)
    {
        Name = name;
    }
}