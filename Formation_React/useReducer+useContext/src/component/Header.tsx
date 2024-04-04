import { useState, useCallback, useEffect } from 'react'

import { MfCancelButton, MfClearButton, MfDialogActions, MfDialogTitle, MfFormField, MfNumberField, MfSearchButton, _mfPopup } from "@mf/react-mui-controls"
import { MfDialog, MfDialogContent, MfButton, MfAutocompletionSimple, MfSvgIcon } from "@mf/react-mui-controls"

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
	const resetCriteria_cb = useCallback(() => { setCriteria({ nbLine: 0, portType: undefined }) }, []);
	const setCriteriaPortType = useCallback((value) => { setCriteria({ ...criteria, portType: value?.label }); }, [criteria])
	const setCriteriaNumber = useCallback((value) => setCriteria({ ...criteria, nbLine: value === null ? undefined : value }), [criteria])

	useEffect(() => { findType() }, [findType])

	return (
		<div className='cHeader' >
			<div className='cHeaderLeft'>
				<h1>Formation React , {number}</h1>
			</div>
			<div className='cHeaderRight'>
				<MfButton startIcon={<MfSvgIcon d={mdiMagnify} />} variant="contained" onClick={findData}>Search</MfButton>
				<MfButton startIcon={<MfSvgIcon d={mdiCog} />} variant="contained" onClick={handleClickOpen} >Criteria</MfButton>
			</div>
			<MfDialog
				open={open}
				onClose={handleClickClose}
				fullWidth
			>
				<MfDialogTitle>
					<p>Criteria Setting</p>
				</MfDialogTitle>
				<MfDialogContent className='dialogContent'>
					<MfFormField label={"Nb Lines"}>
						<MfNumberField
							fullWidth
							value={criteria?.nbLine}
							onChange={setCriteriaNumber}
						/>
					</MfFormField>
					<MfFormField label={"Select a port type"} >
						<MfAutocompletionSimple
							fullWidth
							options={portTypes}
							value={portTypes.find((e) => e.label == criteria.portType)}
							onChange={setCriteriaPortType}
						/>
					</MfFormField>
				</MfDialogContent>
				< MfDialogActions >
					<MfSearchButton onClick={findData} variant="contained" />
					<MfClearButton onClick={resetCriteria_cb} variant="contained" />
					<MfCancelButton onClick={handleClickClose} variant="contained" />
				</MfDialogActions>
			</MfDialog>
		</div >
	)
}

export default Dialog