using AutoMapper;
using DomainModel.Entities.ViewModels;
using FluentValidation;
using Goods.System.Social.Network.DAL.Repository.Interface;
using Goods.System.Social.Network.DomainModel.Entities;
using Goods.System.Social.Network.DomainServices.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Goods.System.Social.Network.DomainServices.Realization
{
    public class PostService : IPostService
    {
        private readonly int _countPosts = 4;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IImagePostRepository _imagePostRepository;
        private readonly ILogger<PostService> _logger;
        private readonly IValidator<int> _idValidator;
        private readonly IValidator<Post> _postValidator;
        private readonly IMapper _mapper;
        public PostService(IPostRepository postRepository, 
                           IMapper mapper, 
                           IImagePostRepository imagePostRepository,
                           IValidator<int> idValidator,
                           IUserRepository userRepository,
                           IValidator<Post> postValidator,
                           ILogger<PostService> logger) 
        { 
            _logger = logger;
            _postValidator = postValidator;
            _postRepository = postRepository;
            _imagePostRepository = imagePostRepository;
            _idValidator = idValidator;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Post> GetAsync(int postId)
        {
            _logger.LogTrace($"Вызван метод GetAsync с параметрами: postId: {postId}");

            var result = await _idValidator.ValidateAsync(postId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            var post = await _postRepository.GetAsync(postId);
            return post;
        }
        public async Task<PagePost> GetAllAsync(string search, int userId, int number)
        {
            _logger.LogTrace($"Вызван метод GetAllAsync с параметрами: search: {search}, userId: {userId}, number: {number}");

            List<Post> posts = await _postRepository.GetAllAsync();
            if (search != "" || userId != 0)
            {
                if (search != "" && userId == 0)
                {
                    posts = posts.Where(p => p.Title.Contains(search)).ToList();
                }
                else if (search == "" && userId != 0)
                {
                    posts = posts.Where(p => p.UserId == userId).ToList();
                }
                else
                {
                    posts = posts.Where(p => p.UserId == userId && p.Title.Contains(search)).ToList();
                }
            }  
            
            posts.Reverse();

            PagePost pagePostViewModel = new PagePost(posts.Count,
                (int)Math.Ceiling((double)posts.Count / _countPosts), 
                number + 1,
                posts.Skip(number * _countPosts).Take(_countPosts).ToList());

            return pagePostViewModel;
        }
        public async Task<int> CreateAsync(Post post, IFormFile image)
        {
            _logger.LogTrace($"Вызван метод CreateAsync с параметрами: post: {post}, image: {image}");

            var result = await _postValidator.ValidateAsync(post);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            _postRepository.Create(post);
            var postsByUser = await _postRepository.GetByUserAsync(post.UserId);
            post = postsByUser[postsByUser.Count - 1];
            await _imagePostRepository.CreateAsync(image, post.UserId, post.Id);
            post.Image = _imagePostRepository.Get(post.UserId, post.Id);
            _postRepository.Update(post);
            return post.Id;
        }

        public void Delete(int userId, int postId)
        {
            var result = _idValidator.Validate(userId);
            if (!result.IsValid) throw new ValidationException(result.Errors);
            result = _idValidator.Validate(postId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            _postRepository.DeleteAsync(postId);
            _imagePostRepository.Delete(userId, postId);
        }

        public async Task UpdateAsync(Post post, IFormFile image)
        {
            _logger.LogTrace($"Вызван метод UpdateAsync с параметрами: Post: {post}");

            var result = await _postValidator.ValidateAsync(post);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            var postOld = await _postRepository.GetAsync(post.Id);
            postOld.Text = post.Text;
            postOld.Title = post.Title;
            postOld.Tags = post.Tags;
            if (image is not null)
            {
                await _imagePostRepository.UpdateAsync(image, postOld.UserId, postOld.Id);
                postOld.Image = _imagePostRepository.Get(postOld.UserId, postOld.Id);
            }
            _postRepository.Update(postOld);
        }

        public async Task<List<Post>> GetByUserAsync(int userId)
        {
            _logger.LogTrace($"Вызван метод GetByUserAsync с параметрами: userId: {userId}");

            var result = await _idValidator.ValidateAsync(userId);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            return await _postRepository.GetByUserAsync(userId);
        }
    }
}