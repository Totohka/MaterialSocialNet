import Image from 'next/image';
import { LogoPng } from '@socnet/assets';
import { LoginForm } from '../components';
import styles from './Auth.module.scss';

export const LoginPageComposition = () => (
  <div className={styles.login}>
    <div className={styles.formWrapper}>
      <Image className={styles.image} src={LogoPng} alt="logo" />
      <LoginForm />
    </div>
  </div>
);
