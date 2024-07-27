import React from 'react';
import SettingAccount from './SettingAccount';
import { connect } from 'react-redux';
import { changeCityActionCreator, changeCountryActionCreator, changeDataBithdayActionCreator, changeStatusActionCreator, changeFirstNameActionCreator, changeLastNameActionCreator, changeScaleActionCreator } from '../../../redux/settings-account-reducer';

let mapStateToProps = (state) => {
    return{
        firstName: state.settingAccount.firstName,
        lastName: state.settingAccount.lastName,
        city: state.settingAccount.city,
        country: state.settingAccount.country,
        dataBithday: state.settingAccount.dataBithday,
        countScale: state.settingAccount.countScale,
        status: state.settingAccount.status
    }
}

let mapDispatchToProps = (dispatch) => {
    return{
        updateNewFirstName: (firstName) => {
            dispatch(changeFirstNameActionCreator(firstName));
        },
        updateNewLastName: (lastName) => {
            dispatch(changeLastNameActionCreator(lastName));
        },
        updateNewCity: (city) => {
            dispatch(changeCityActionCreator(city));
        },
        updateNewCountry: (country) => {
            dispatch(changeCountryActionCreator(country));
        },
        updateNewDataBithday: (dataBithday) => {
            dispatch(changeDataBithdayActionCreator(dataBithday));
        },
        updateNewCountScale: (countScale) => {
            dispatch(changeScaleActionCreator(countScale));
        },
        updateNewStatus: (status) => {
            dispatch(changeStatusActionCreator(status));
        }
    }
}

const SettingAccountContainer = connect(mapStateToProps, mapDispatchToProps)(SettingAccount);

export default SettingAccountContainer;