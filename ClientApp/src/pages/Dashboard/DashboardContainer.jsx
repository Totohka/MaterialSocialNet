import React from 'react';
import Dashboard from './Dashboard';
import { connect } from 'react-redux';
import { setVisitInDashboardActionCreator } from '../../redux/dashboard-reducer';

let mapStateToProps = (state) => {
    return{
        visitToDay: state.dashboardPage.visitToDay,
        visitToMonth: state.dashboardPage.visitToMonth,
        visitToYear: state.dashboardPage.visitToYear
    }
}

let mapDispatchToProps = (dispatch) => {
    return{
        setDataForDashboard: (day, month, year) => {
            dispatch(setVisitInDashboardActionCreator(day, month, year));
        }
    }
}

const DashboardContainer = connect(mapStateToProps, mapDispatchToProps)(Dashboard);

export default DashboardContainer;