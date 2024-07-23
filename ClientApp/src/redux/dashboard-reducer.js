const SET_VISITS_IN_DASHBOARD = 'ADD-PHOTO-IN-GALLERY';
// const CHANGE_PHOTO_IN_GALLERY = 'CHANGE-PHOTO-IN-GALLERY';

const initialState = { 
  visitToDay: [],
  visitToMonth: [],
  visitToYear: []
}

const dashboardReducer = (state = initialState, action) =>{
  switch(action.type){
    case SET_VISITS_IN_DASHBOARD:{
      let stateCopy = {...state};
      stateCopy.visitToDay = action.visitToDay;
      stateCopy.visitToMonth = action.visitToMonth;
      stateCopy.visitToYear = action.visitToYear;
      return stateCopy;
    }   
    default:
        return state;
  }   
} 

export const setVisitInDashboardActionCreator = (day, month, year) =>{
  return{
    type: SET_VISITS_IN_DASHBOARD,
    visitToDay: day,
    visitToMonth: month,
    visitToYear: year
  }
}

export default dashboardReducer;