﻿using _0_Framework.Application;

namespace InventoryManagement.Application.Contracts.Inventory;

public interface IInventoryApplication
{
    OperationResult Create(CreateInventory command);
    OperationResult Edit(EditInventory command);
    OperationResult Increase(IncreaseInventory command);
    OperationResult Increase(List<IncreaseInventory> command);
    OperationResult Reduce(List<ReduceInventory> command);
    OperationResult Reduce(ReduceInventory command);
    EditInventory GetDetails(long id);
    List<InventoryViewModel> Search(InventorySearchModel searchModel);
    List<InventoryOperationViewModel> GetLog(long inventoryId);
}