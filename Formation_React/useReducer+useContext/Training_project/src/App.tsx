import { useState, } from 'react';
import { Details, Grid, Header } from './component/componentMain'
import { GridApi } from '@mf/react-grid';


// eslint-disable-next-line @typescript-eslint/no-explicit-any
export default function App() {
	const [gridApi, setGridApi] = useState<GridApi | undefined>(undefined);
	return (
		<div className='App'>
			<Header gridApi={gridApi} />
			<div className='cAppBody'>
				<Grid setGridApi={setGridApi} />
				<Details />
			</div>
		</div>
	);
}