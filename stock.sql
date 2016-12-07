select stock.ProductID,stock.ShadeID,stock.PackingID,prev.AvailableQty as OpeningQty,SUM(Case when InOut=0 then Stock.Qty else 0 end) as INQty
,SUM(Case when InOut=0 then 0 else (-1 * Qty) end) as OUTQty,
ISNULL(SUM(Case when InOut=0 then Stock.Qty else -1 * Stock.Qty end),0) + ISNULL(prev.AvailableQty,0) as AvailableQty
 from stock
left join (
select ProductShade.ProductID,ProductPacking.PackingID,ProductShade.ShadeID,ProductShade.OpeningQty from ProductShade
left join ProductPacking on ProductPacking.ID = ProductShade.ProductPackingID
) PS on PS.ProductID=Stock.ProductID and PS.PackingID=stock.PackingID and PS.ShadeID=stock.ShadeID
left join (
select stock.ProductID,stock.ShadeID,stock.PackingID,ISNULL(PS.OpeningQty,0) as OpeningQty,SUM(Case when InOut=0 then Stock.Qty else 0 end) as INQty
,SUM(Case when InOut=0 then 0 else (-1 * Qty) end) as OUTQty,
ISNULL(SUM(Case when InOut=0 then Stock.Qty else -1 * Stock.Qty end),0) + ISNULL(PS.OpeningQty,0) as AvailableQty
 from stock
left join (
select ProductShade.ProductID,ProductPacking.PackingID,ProductShade.ShadeID,ProductShade.OpeningQty from ProductShade
left join ProductPacking on ProductPacking.ID = ProductShade.ProductPackingID
) PS on PS.ProductID=Stock.ProductID and PS.PackingID=stock.PackingID and PS.ShadeID=stock.ShadeID

group by stock.ProductID,stock.ShadeID,stock.PackingID,PS.OpeningQty
) prev on prev.ProductID=Stock.ProductID and prev.PackingID=stock.PackingID and prev.ShadeID=stock.ShadeID
where InOutDate < '2016-09-24' and stock.ProductID=12 and stock.ShadeID=1 and stock.PackingID=7
group by stock.ProductID,stock.ShadeID,stock.PackingID,prev.AvailableQty

--select ProductID,ShadeID,PackingID,SUM(Case when InOut=0 then Qty else -1 * Qty end) as AvailableQty from stock
--group by ProductID,ShadeID,PackingID

--select * from stock

