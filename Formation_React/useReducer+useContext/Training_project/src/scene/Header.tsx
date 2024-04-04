import { useState, useCallback, useEffect } from 'react'

import { _mfPopup } from "@mf/react-mui-controls"
import { MfButton, MfSvgIcon } from "@mf/react-mui-controls"

import { mdiMagnify, mdiCog, } from '@mdi/js';

import demoService from '../services/demoService';
import { IHeaderProps, IPortTypes, IParams } from "../interfaces/_interfacesMain"
import { ActionEnum } from '../reducer/gridReducer';
import { useGridDataContext } from '../reducer/GridContext';


function Dialog(props: IHeaderProps) {
	const { gridData, dispatch } = useGridDataContext()!;
	const { number } = gridData;
	const { gridApi } = props;
	const [open, setOpen] = useState(false);
	const [criteria, setCriteria] = useState<IParams>({ nbLine: undefined, portType: undefined })
	const [portTypes, setPortTypes] = useState<IPortTypes[]>([]);

	const handleClickOpen = useCallback(() => setOpen(true), []);
	const handleClickClose = useCallback(() => setOpen(false), []);

	const findPortTypeCode = useCallback((criteria) => portTypes.find((e) => e.label === criteria.portType)?.code, [portTypes])
	const findType = useCallback(() => {
		demoService.GetOptionsPortType().promise.then(r => {
			if (r.ok) {
				setPortTypes(r.data.data);
			} else {
				_mfPopup.displayMessage({ content: ["Error during Api Call", "Please Contact Admin.."], state: "error" });
			}
		});
	}, [])

	const findData = useCallback(() => {
		gridApi?.showLoadingOverlay();
		demoService.getData({ nbLine: criteria.nbLine == 0 ? undefined : criteria.nbLine, portType: findPortTypeCode(criteria) }).promise.then(r => {
			if (r.ok) {
				_mfPopup.displayMessage({ content: ["API called with success"], state: "success" });
				dispatch({ type: ActionEnum.setData, value: r.data.data })
				setOpen(false);
			}
			else {
				_mfPopup.displayMessage({ content: ["Error during Api Call", "Please Contact Admin.."], state: "error" });
			}
			gridApi?.hideOverlay();
		});
	}, [criteria, gridApi, findPortTypeCode, dispatch])
	useEffect(() => { findType() }, [findType])

	return (
		<div className='cHeader' >
			<div className='cHeaderLeft'>
				<h1>Formation React , {number}</h1>
			</div>
			<div className='cHeaderRight'>
				<MfButton startIcon={<MfSvgIcon d={mdiMagnify} />} variant="contained" onClick={findData}>Search</MfButton>
				<MfButton startIcon={<MfSvgIcon d={mdiCog} />} variant="contained" onClick={handleClickOpen}>Criteria</MfButton>
			</div>

		</div >
	)
}

export default Dialog