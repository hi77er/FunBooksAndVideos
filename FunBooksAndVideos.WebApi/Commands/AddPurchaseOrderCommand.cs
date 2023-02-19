﻿using FunBooksAndVideos.DAL.Entities;
using MediatR;

namespace FunBooksAndVideos.WebApi.Commands
{
    public record AddPurchaseOrderCommand(PurchaseOrder PurchaseOrder) : IRequest<Unit>;
}
