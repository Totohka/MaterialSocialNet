import { PropsWithChildren } from 'react';
import styles from './WithoutLayout.module.scss';

export const WithoutLayout = ({ children }: PropsWithChildren) => (
  <main>{children}</main>
);
