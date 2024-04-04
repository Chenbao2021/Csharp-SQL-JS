import { useCallback, useMemo } from 'react'
import { ColDef, GridReadyEvent, MfGridBase } from '@mf/react-grid';
import { IRow, IListReferentialProps } from '../interfaces/_interfacesMain'
import { ActionEnum } from '../reducer/gridReducer';
import { useGridDataContext } from '../reducer/GridContext';
// Row Data Interface
function Grid(props: IListReferentialProps) {
	const { gridData, dispatch } = useGridDataContext()!;
	const { data } = gridData;

	const { setGridApi } = props;
	const defaultColDef = useMemo(() => {
		return {
			flex: 1
		}
	}, []);
	// Column Definitions: Defines the columns to be displayed.
	const colDefs: ColDef[] = useMemo(() => [
		{
			headerName: "ID",
			field: "id",
		},
		{
			headerName: "Name",
			field: "name"
		},
		{
			headerName: "Alias",
			field: "alias"
		},
		{
			headerName: "Type",
			field: "type.label"
		},
		{
			headerName: "Code",
			field: "code"
		},
		{
			headerName: "Country",
			field: "country.name"
		},
		{
			headerName: "Latitude",
			field: "coordinates.latitude"
		},
		{
			headerName: "Longitude",
			field: "coordinates.longitude"
		},
		{
			headerName: "Cancelled",
			field: "cancelled"
		},
	], []);
	const setDetailValue = useCallback((data?: IRow) => {
		dispatch({ type: ActionEnum.setSelectedData, value: data })
	}, [dispatch]);
	const rowSelected_cb = useCallback((e) => { e.node.isSelected() && setDetailValue(e.data) }, [setDetailValue]);
	const setGridApi_cb = useCallback((e: GridReadyEvent) => setGridApi(e.api), [setGridApi]);

	return (
		<>
			<MfGridBase
				className='cListReferential'
				defaultColDef={defaultColDef}
				rowData={data}
				columnDefs={colDefs}
				onRowSelected={rowSelected_cb}
				onGridReady={setGridApi_cb}
				rowSelection='single'
			/>
		</>

	)
}

export default Grid