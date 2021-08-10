
using _0_FrameWork.BaseClass;
using Comment.Managment.Cantract.Comment;
using System.Collections.Generic;

namespace Commentmanagment.Domain.CommentAgg
{
   public interface ICommentRepository:IRepositoryBaseClass<long,Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
