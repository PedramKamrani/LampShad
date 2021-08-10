using _0_FrameWork.BaseClass;
using _0_FrameWork.RepositoryBase;
using Comment.Managment.Cantract.Comment;
using Commentmanagment.Domain.CommentAgg;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CommenetManagmenrt.Infractracer.EFCore.Repository
{
   public class CommentRepository:RepositoryBaseClass<long, Commentmanagment.Domain.CommentAgg.Comment>,ICommentRepository
    {
        private readonly CommentContext _context;
        public CommentRepository(CommentContext context):base(context)
        {
            _context = context;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _context.Comments.Select(x=>new CommentViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Website = x.Website,
                Message = x.Message,
                OwnerRecordId = x.OwnerRecordId,
                Type = x.Type,
                IsCanceled = x.IsCanceled,
                IsConfirmed = x.IsConfirmed,
                CommentDate = x.Creation.ToFarsi()

            });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x=>EF.Functions.Like(x.Name,$"%{x.Name}%"));
                //query = query.Where(x=>x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(x => EF.Functions.Like(x.Email, $"%{x.Email}%"));

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
