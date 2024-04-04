import { useCallback, useEffect } from 'react';
import { MfButton, _errorUtil, _mfPopup } from "@mf/react-mui-controls"
import demoService from '../services/demoService';

export interface ITestComponentsProps {
	customButtonName: string;
}


export default function TestComponents(props: ITestComponentsProps): JSX.Element {

	const { customButtonName } = props;


	const callApiTest = useCallback(() => {
		demoService.testPing().promise.then(r => {
			if (r.ok)
				_mfPopup.displayMessage({ content: ["API called with success"], state: "success" });
			else {
				_mfPopup.displayMessage({ content: ["Error during Api Call", "Please Contact Admin.."], state: "error" });
			}
		});
	}, []);

	useEffect(() => {
		callApiTest();
	});

	const onGenerateToast = useCallback(() => {
		_mfPopup.displayMessage({ content: ["I'm a Toast"], state: "info" });
	}, []);
	const onGenerateError = useCallback(() => {
		_errorUtil.logErrorMsg("I'm an Error", "Content Error");
	}, []);
	const onTestApi = useCallback(() => {
		callApiTest()
	}, [callApiTest]);

	return (
		<>
			<MfButton className='myCustomButton' >{customButtonName}</MfButton>
			<MfButton onClick={onGenerateToast}>Generate Toast</MfButton>
			<MfButton onClick={onGenerateError}>Generate Error</MfButton>
			<MfButton onClick={onTestApi}>Test Api</MfButton>
		</>
	);
}