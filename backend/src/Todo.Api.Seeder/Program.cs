using Todo.Api.Seeder;

try
{
    await ApiDropper.DropAsync();
}
finally
{
    await ApiSeeder.SeedAsync();
}