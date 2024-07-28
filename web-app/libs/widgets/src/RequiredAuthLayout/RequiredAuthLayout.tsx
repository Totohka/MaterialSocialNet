'use client';

import { redirect } from 'next/navigation';
import { PropsWithChildren, useLayoutEffect } from 'react';
import { getRoute, RoutesEnum } from '@socnet/shared';

export const RequiredAuthLayout = ({ children }: PropsWithChildren) => {
  useLayoutEffect(() => {
    const isAuth = !!localStorage.getItem('token');

    if (!isAuth) {
      return redirect(getRoute(RoutesEnum.Login));
    }
  }, []);

  return children;
};
