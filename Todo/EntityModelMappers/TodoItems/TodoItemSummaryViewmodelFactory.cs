using Todo.Data.Entities;
using Todo.Models.TodoItems;
using Todo.Services;

namespace Todo.EntityModelMappers.TodoItems
{
    public static class TodoItemSummaryViewmodelFactory
    {

        public  static TodoItemSummaryViewmodel Create(TodoItem ti, string gravatarName)
        {
            return new TodoItemSummaryViewmodel(ti.TodoItemId, ti.Title, ti.IsDone, UserSummaryViewmodelFactory.Create(ti.ResponsibleParty), ti.Importance, ti.Rank, gravatarName);
        }
    }
}