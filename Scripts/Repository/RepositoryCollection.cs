using Lab2;

public class RepositoryCollection {
    public IItemRepository ItemRepository { get; init; }
    public IUserRepository UserRepository { get; init; }
    public IRatingMapRepository RatingMapRepository { get; init; }

    public RepositoryCollection(IItemRepository itemRepository, IUserRepository userRepository, IRatingMapRepository ratingMapRepository) {
        ItemRepository = itemRepository;
        UserRepository = userRepository;
        RatingMapRepository = ratingMapRepository;
    }
}