﻿using _01_SogandShopQuery.Contracts.Slide;
using ShopManagement.Infrastructure.EFCore;

namespace _01_SogandShopQuery.Query;

public class SlideQuery:ISlideQuery
{
    private readonly ShopContext _shopContext;

    public SlideQuery(ShopContext context)
    {
        _shopContext = context;
    }

    public List<SlideQueryModel> GetSlides()
    {
        return _shopContext.Slides
            .Where(x => x.IsRemoved == false)
            .Select(x => new SlideQueryModel()
            {
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Heading = x.Heading,
                Text = x.Text,
                Title = x.Title,
                Link = x.Link,
                BtnText = x.BtnText
            })
            .ToList();
    }
}