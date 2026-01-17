namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Categories, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
public record UpdateProductResult(Guid Id);

internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product with Id {$Request}.", request);
        var product = await session.LoadAsync<Product>(request.Id, cancellationToken);

        if (product is null)
        {
            logger.LogWarning("Product with Id {ProductId} not found.", request.Id);
            throw new ProductNotFoundException();
        }

        product.Name = request.Name;
        product.Categories = request.Categories;
        product.Description = request.Description;
        product.ImageFile = request.ImageFile;
        product.Price = request.Price;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Product with Id {ProductId} updated successfully.", request.Id);

        return new UpdateProductResult(request.Id);
    }
}