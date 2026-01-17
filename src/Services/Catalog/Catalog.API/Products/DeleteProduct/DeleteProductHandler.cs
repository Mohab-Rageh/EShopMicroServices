namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(Guid Id);

public class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting product with Id: {ProductId}", request.Id);
        var product = await session.LoadAsync<Product>(request.Id, cancellationToken);

        if (product is null)
        {
            logger.LogWarning("Product with Id: {ProductId} not found", request.Id);
            throw new ProductNotFoundException();
        }

        session.Delete(product);
        await session.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Product with Id: {ProductId} deleted successfully", request.Id);

        return new DeleteProductResult(request.Id);
    }
}