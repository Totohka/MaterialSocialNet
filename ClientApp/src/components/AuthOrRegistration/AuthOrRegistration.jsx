import { React, useLayoutEffect, useEffect } from 'react';
import './AuthOrRegistration.css';
import { NavLink } from 'react-router-dom';

const AuthOrRegistration = () => {
    
    useLayoutEffect(() => {
     document.getElementById('gray_space').style.backgroundColor = 'black';
    }, []);

    return (
        <div class="formAuthOrRegistration">
        <div>
          <div class="textAuthOrRegistration">
            <article>
              <h1 class="titleAuthOrRegistration">Банда</h1>
              <div></div>
              <h3 class="helloAuthOrRegistration">Приветствует вас</h3>
            </article>
          </div>  
          <div className='boxForAuthOrRegistration'>     
            <div className='buttonAuthOrRegistration buttonReg'>
                <NavLink to='/registration'>Регистрация</NavLink>
            </div>
            <div></div>
            <div className='buttonAuthOrRegistration buttonAuth'>
                <NavLink to='/login'>Войти</NavLink>
            </div>
          </div>  
        </div>
      </div>
    );
}

export default AuthOrRegistration;