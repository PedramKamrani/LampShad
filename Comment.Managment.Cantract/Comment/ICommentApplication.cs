using _0_FrameWork.BaseClass;
using System.Collections.Generic;


namespace Comment.Managment.Cantract.Comment
{
   public interface ICommentApplication
    {
        OperationResult Add(AddComment command);
        OperationResult Confirm(long id);
        OperationResult Cancel(long id);
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
