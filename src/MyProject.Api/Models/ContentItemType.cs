using GraphQL.Types;
using MyProject.Core.Entities;
using MyProject.Core.Repositories;
using System.Collections.Generic;

namespace MyProject.Api.Models
{
    public class ContentItemType : ObjectGraphType<ContentItem>
    {
        public ContentItemType(IContentItemRepository contentItemRepository)
        {
            Field(x => x.Tenant, nullable:true);
            Field(x => x.Id, nullable:true);
            Field(x => x.Name, nullable:true);
            Field(x => x.CreatedBy, nullable:true);
            Field(x => x.CreatedDate, nullable:true);
            Field(x => x.CreatedDateTicks, nullable:true);
            Field(x => x.UpdatedBy, nullable:true);
            Field(x => x.UpdatedDate, nullable:true);
            Field(x => x.UpdatedDateTicks, nullable:true);
            Field(x => x.ParentId, nullable:true);
            Field(x => x.ChildCount, nullable:true);

            Field(x => x.Alias, nullable:true);
            Field(x => x.Module, nullable:true);
            Field(x => x.Type, nullable:true);
            Field(x => x.SectionItems, nullable:true);
            Field(x => x.Content, nullable:true);
            Field(x => x.HtmlContent, nullable:true);
            Field(x => x.Snippet, nullable:true);
            Field(x => x.Attribute01, nullable:true);
            Field(x => x.Attribute02, nullable:true);
            Field(x => x.Attribute03, nullable:true);
            Field(x => x.Attribute04, nullable:true);
            Field(x => x.Attribute05, nullable:true);
            Field(x => x.Attribute06, nullable:true);
            Field(x => x.Attribute07, nullable:true);
            Field(x => x.Attribute08, nullable:true);
            Field(x => x.Attribute09, nullable:true);
            Field(x => x.Attribute10, nullable:true);
            Field(x => x.Attribute11, nullable:true);
            Field(x => x.Attribute12, nullable:true);
            Field(x => x.Attribute13, nullable:true);
            Field(x => x.Attribute14, nullable:true);
            Field(x => x.Attribute15, nullable:true);
            Field(x => x.Attribute16, nullable:true);
            Field(x => x.Attribute17, nullable:true);
            Field(x => x.Attribute18, nullable:true);
            Field(x => x.Attribute19, nullable:true);
            Field(x => x.Attribute20, nullable:true);

            Field<ListGraphType<ContentSectionItemType>, IEnumerable<ContentSectionItem>> ()
                .Name("ContentSectionItems")
                .ResolveAsync(async x => {
                    if (x != null)
                    {
                        return await contentItemRepository.GetContentSectionItemsByContentItemId(x.Source.Id);
                    }
                    else
                    {
                        return new List<ContentSectionItem>();
                    }
                });        
        }
    }
}