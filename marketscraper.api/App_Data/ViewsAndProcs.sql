--DROP FUNCTION dbo.GetStatistics;

CREATE FUNCTION dbo.GetStatistics
	(
		@loadId AS int,
		@minRatio AS float,
		@minOrderVolume AS float
	)
RETURNS TABLE
AS
RETURN
	select
		mt.MarketTypeId,
		mt.Name,
		(sell.MinSellPrice / buy.MaxBuyPrice) as Ratio,
		(history.AvgOrderVolume * sell.MinSellPrice) as AvgSellMarketSize,
		(history.AvgOrderVolume * buy.MaxBuyPrice) as AvgBuyMarketSize,
		buy.MaxBuyPrice,
		sell.MinSellPrice,
		buy.BuyRemainVolume,
		buy.BuyTotalVolume,
		sell.SellRemainVolume,
		sell.SellTotalVolume,
		history.AvgOrderCount,
		history.AvgOrderVolume
	from MarketType mt
	left join
		(select
			mo.LoadId,
			TypeId,
			MAX(CAST(mo.Price as float)) MaxBuyPrice,
			SUM(CAST(mo.VolumeTotal as float)) BuyTotalVolume,
			SUM(CAST(mo.VolumeRemain as float)) BuyRemainVolume
		from MarketOrder mo
		where mo.LoadId = @loadId and LocationId = 60003760 and IsBuyOrder = 1
		group by mo.LoadId, TypeId) buy on buy.TypeId = mt.MarketTypeId
	left join
		(select
			mo.LoadId,
			TypeId,
			MIN(CAST(mo.Price as float)) MinSellPrice,
			SUM(CAST(mo.VolumeTotal as float)) SellTotalVolume,
			SUM(CAST(mo.VolumeRemain as float)) SellRemainVolume
		from MarketOrder mo
		where mo.LoadId = @loadId and LocationId = 60003760 and IsBuyOrder = 0
		group by mo.LoadId, TypeId) sell on sell.TypeId = mt.MarketTypeId
	left join
		(select
			moh.MarketTypeId,
			AVG(moh.OrderCount) AvgOrderCount,
			AVG(moh.Volume) AvgOrderVolume
		from MarketOrderHistory moh
		group by moh.MarketTypeId) history on history.MarketTypeId = mt.MarketTypeId
	where 
		not mt.Name like '%Blueprint'
		and not mt.Name like '% SKIN%'
		and (sell.MinSellPrice / buy.MaxBuyPrice) > @minRatio
		and history.AvgOrderVolume > @minOrderVolume
